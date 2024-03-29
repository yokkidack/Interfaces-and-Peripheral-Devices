#pragma comment (lib, "Setupapi.lib")
#include <stdio.h>
#include <windows.h>
#include <setupapi.h>
#include <devguid.h>
#include <regstr.h>
#include <locale.h>
#include <list>
#include <iterator>
#include <iostream>
#include <string>
#include <fstream>
#include <limits>
#include "pci0.h"
using namespace std;

void init(list<string>&, HDEVINFO&);
list<_PCI_DEVTABLE> pciSearch(list<_PCI_DEVTABLE>&);
list<_PCI_DEVTABLE> subString(list<string>);
void print(list<_PCI_DEVTABLE>&);

int main(int argc, char *argv[])
{
	list<string> devices;
	HDEVINFO hDevInfo;
	init(devices, hDevInfo);
	list<_PCI_DEVTABLE> result = pciSearch(subString(devices));
	SetupDiDestroyDeviceInfoList(hDevInfo);
	print(result);

	return 0;
}

void init(list<string>& devices, HDEVINFO& hDevInfo)
{
	SP_DEVINFO_DATA deviceInfoData;
	deviceInfoData.cbSize = sizeof(SP_DEVINFO_DATA);

	if ((hDevInfo = SetupDiGetClassDevs(NULL, REGSTR_KEY_PCIENUM, 0, DIGCF_PRESENT | DIGCF_ALLCLASSES)) == INVALID_HANDLE_VALUE)
	{
		exit(1);
	}

	for (DWORD i = 0; (SetupDiEnumDeviceInfo(hDevInfo, i, &deviceInfoData)); i++)
	{
		DWORD data;
		LPTSTR buffer = NULL;
		DWORD bufferSize = 0;

		while (!SetupDiGetDeviceRegistryProperty(hDevInfo,&deviceInfoData,SPDRP_HARDWAREID,&data,(PBYTE)buffer,bufferSize,&bufferSize))
		{
			if (GetLastError() == ERROR_INSUFFICIENT_BUFFER)
			{
				if (buffer)
				{
					LocalFree(buffer);
				}
				buffer = (LPTSTR)LocalAlloc(LPTR, bufferSize * 2);
			}
			else
			{
				break;
			}
		}

		devices.push_back((string)buffer);

		if (buffer)
		{
			LocalFree(buffer);
		}
	}

	
}

void print(list<_PCI_DEVTABLE> &temp)
{
	int i = 0;
	while (temp.size() != 0)
	{
		cout << i + 1 << ".VendorID: " << temp.front().VenId << " DeviceID: " << temp.front().DevId << endl;
		cout << " Chip : " << temp.front().Chip << " Description: " << temp.front().ChipDesc << endl;
		temp.pop_front();
		i++;
	}
}

list<_PCI_DEVTABLE> subString(list<string> str)
{
	list<_PCI_DEVTABLE> fullInfo;
	while (str.size() != 0)
	{
		_PCI_DEVTABLE temp;
		string vendor = str.front().substr(8, 4);
		string device = str.front().substr(17, 4);
		string result0 = "0x" + vendor;
		string result1 = "0x" + device;
		temp = { result0, result1, NULL, NULL };
		fullInfo.push_front(temp);
		str.pop_front();
	}
	return fullInfo;
}

list<_PCI_DEVTABLE> pciSearch(list<_PCI_DEVTABLE>& temp)
{
	list<_PCI_DEVTABLE> completeList;
	while (temp.size() != 0)
	{
		for (int i = 0; i < PCI_DEVTABLE_LEN; i++)
		{
			if (temp.front().VenId == PciDevTable[i].VenId && temp.front().DevId == PciDevTable[i].DevId)
			{
				completeList.push_front(PciDevTable[i]);
				break;
			}
		}
		temp.pop_front();
	}
	return completeList;
}
