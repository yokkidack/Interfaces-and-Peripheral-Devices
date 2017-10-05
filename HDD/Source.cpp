#include <iostream>
#include <iomanip>
#include <Windows.h>
#include <WinIoCtl.h>
#include <ntddscsi.h>
#include <conio.h>

using namespace std;

#define bThousand 1024
#define Hundred 100
#define BYTE_SIZE 8

char* busType[] = { "UNKNOWN", "SCSI", "ATAPI", "ATA", "ONE_TREE_NINE_FOUR", "SSA", "FIBRE", "USB", "RAID", "ISCSI", "SAS", "SATA", "SD", "MMC" };

void getMemotyInfo() {
	string path;
	_ULARGE_INTEGER diskSpace;
	_ULARGE_INTEGER freeSpace;

	diskSpace.QuadPart = 0;
	freeSpace.QuadPart = 0;

	_ULARGE_INTEGER totalDiskSpace;
	_ULARGE_INTEGER totalFreeSpace;
	totalDiskSpace.QuadPart = 0;
	totalFreeSpace.QuadPart = 0;

	unsigned long int logicalDrivesCount = GetLogicalDrives();
	cout.setf(ios::left);
	cout << setw(8) << "Disk"
		<< setw(16) << "Total space[Mb]"
		<< setw(16) << "Free space[Mb]"
		<< setw(16) << "Busy space[%]"
		<< endl;

	for (char var = 'A'; var < 'Z'; var++) {
		if ((logicalDrivesCount >> var - 65) & 1 && var != 'F') {
			path = var;
			path.append(":\\");
			GetDiskFreeSpaceEx(path.c_str(), 0, &diskSpace, &freeSpace);
			diskSpace.QuadPart = diskSpace.QuadPart / (bThousand * bThousand);
			freeSpace.QuadPart = freeSpace.QuadPart / (bThousand * bThousand);

			if (GetDriveType(path.c_str()) == 3) {
				totalDiskSpace.QuadPart += diskSpace.QuadPart;
				totalFreeSpace.QuadPart += freeSpace.QuadPart;
			}
		}
	}

	cout << setw(8) << "HDD"
		<< setw(16) << totalDiskSpace.QuadPart
		<< setw(16) << totalFreeSpace.QuadPart
		<< setw(16) << setprecision(3) << 100.0 - (double)totalFreeSpace.QuadPart / (double)totalDiskSpace.QuadPart * Hundred
		<< endl;
}

void getDeviceInfo(HANDLE diskHandle, STORAGE_PROPERTY_QUERY storageProtertyQuery) {
	STORAGE_DEVICE_DESCRIPTOR* deviceDescriptor = (STORAGE_DEVICE_DESCRIPTOR*)calloc(bThousand, 1);
	//Used to retrieve the storage device descriptor data for a device.
	deviceDescriptor->Size = bThousand;

	//Sends a control code directly to a specified device driver
	if (!DeviceIoControl(diskHandle, IOCTL_STORAGE_QUERY_PROPERTY, &storageProtertyQuery, sizeof(storageProtertyQuery), deviceDescriptor, bThousand, NULL, 0)) {
		printf("%d", GetLastError());
		CloseHandle(diskHandle);
		exit(-1);
	}

	cout << "Product ID:    " << (char*)(deviceDescriptor)+deviceDescriptor->ProductIdOffset << endl;
	cout << "Version        " << (char*)(deviceDescriptor)+deviceDescriptor->ProductRevisionOffset << endl;
	cout << "Bus type:      " << busType[deviceDescriptor->BusType] << endl;
	cout << "Serial number: " << (char*)(deviceDescriptor)+deviceDescriptor->SerialNumberOffset << endl;
}

void getMemoryTransferMode(HANDLE diskHandle, STORAGE_PROPERTY_QUERY storageProtertyQuery) {
	STORAGE_ADAPTER_DESCRIPTOR adapterDescriptor;
	if (!DeviceIoControl(diskHandle, IOCTL_STORAGE_QUERY_PROPERTY, &storageProtertyQuery, sizeof(storageProtertyQuery), &adapterDescriptor, sizeof(STORAGE_DESCRIPTOR_HEADER), NULL, NULL)) {
		cout << GetLastError();
		exit(-1);
	}
	else {
		cout << "Transfer mode: ";
		adapterDescriptor.AdapterUsesPio ? cout << "PIO" : cout << "DMA";

		cout << endl;
	}

}

void getAtaSupportStandarts(HANDLE diskHandle) {

	UCHAR identifyDataBuffer[512 + sizeof(ATA_PASS_THROUGH_EX)] = { 0 };

	ATA_PASS_THROUGH_EX &PTE = *(ATA_PASS_THROUGH_EX *)identifyDataBuffer;
	PTE.Length = sizeof(PTE);
	PTE.TimeOutValue = 10;
	PTE.DataTransferLength = 512;
	PTE.DataBufferOffset = sizeof(ATA_PASS_THROUGH_EX);
	PTE.AtaFlags = ATA_FLAGS_DATA_IN;

	IDEREGS *ideRegs = (IDEREGS *)PTE.CurrentTaskFile;
	ideRegs->bCommandReg = 0xEC;

	if (!DeviceIoControl(diskHandle, IOCTL_ATA_PASS_THROUGH, &PTE, sizeof(identifyDataBuffer), &PTE, sizeof(identifyDataBuffer), NULL, NULL)) {
		cout << GetLastError() << std::endl;
		return;
	}

	WORD *data = (WORD *)(identifyDataBuffer + sizeof(ATA_PASS_THROUGH_EX));
	short ataSupportByte = data[80];
	int dma_support = data[88];
	if (dma_support){
		cout << "Transfer mode: dma\n";
	}
	int i = 2 * BYTE_SIZE;
	int bitArray[2 * BYTE_SIZE];
	while (i--) {
		bitArray[i] = ataSupportByte & 32768 ? 1 : 0;
		ataSupportByte = ataSupportByte << 1;
	}

	cout << "ATA Support:   ";
	for (int i = 8; i >= 4; i--) {
		if (bitArray[i] == 1) {
			cout << "ATA" << i;
			if (i != 4) {
				cout << ", ";
			}
		}
	}
	cout << endl;
}

int main() {
	STORAGE_PROPERTY_QUERY storageProtertyQuery; //properties query of a storage device or adapter
	storageProtertyQuery.QueryType = PropertyStandardQuery; // flags indicating the type of query 
	storageProtertyQuery.PropertyId = StorageDeviceProperty; // Indicates whether the caller is requesting a device descriptor

	HANDLE diskHandle = CreateFile("\\.\PhysicalDrive0", GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_EXISTING, NULL, NULL);
	if (diskHandle == INVALID_HANDLE_VALUE) {
		cout << GetLastError();
		return -1;
	}

	getDeviceInfo(diskHandle, storageProtertyQuery);
	getMemoryTransferMode(diskHandle, storageProtertyQuery);
	getAtaSupportStandarts(diskHandle);
	getMemotyInfo();

	return 0;
}