/*
 PCI devices identification. Using C++ and
 SetupAPI. Using external source of VEN&DEV
 for correlation.
*/
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
//#include "header.h"
#include "pci0.h"
using namespace std;

_PCI_DEVTABLE temp;
_PCI_DEVTABLE search(_PCI_DEVTABLE&);

_PCI_DEVTABLE getDeviceInfo(string);
string getInfo(char*, string);

int main(int argc, char *argv[])
{
	setlocale(LC_ALL, "RUSSIAN");
	HDEVINFO hDevInfo;
	SP_DEVINFO_DATA deviceInfoData;
	deviceInfoData.cbSize = sizeof(SP_DEVINFO_DATA);
	list<string> devices;

	if ((hDevInfo = SetupDiGetClassDevs(NULL,
		REGSTR_KEY_PCIENUM,
		0,
		DIGCF_PRESENT | DIGCF_ALLCLASSES)) == INVALID_HANDLE_VALUE)
	exit(1);

	for (DWORD i = 0; (SetupDiEnumDeviceInfo(hDevInfo, i, &deviceInfoData)); i++)
	{
		DWORD data;
		LPTSTR buffer = NULL;
		DWORD bufferSize = 0;

		while (!SetupDiGetDeviceRegistryProperty(
			hDevInfo,
			&deviceInfoData,
			SPDRP_HARDWAREID,
			//SPDRP_DEVICEDESC,
			&data,
			//(PBYTE)buffer,
			(PBYTE)buffer,
			bufferSize,
			&bufferSize))
		{
			if (GetLastError() == ERROR_INSUFFICIENT_BUFFER)//The data area passed to a system call is too small
			{
				if (buffer)
					LocalFree(buffer);
				buffer = (LPTSTR)LocalAlloc(LPTR, bufferSize * 2);
			}
			else
				break;
		}
		
		printf("%d. %s\n", i + 1, buffer);
		devices.push_back((string)buffer);

		if (buffer)
			LocalFree(buffer);
	}


	while (devices.size() != 0)
	{
		_PCI_DEVTABLE q = getDeviceInfo(devices.front());
		search(q);
		devices.pop_front();		
	}
	
	//readCSV(argv[0], a);
	SetupDiDestroyDeviceInfoList(hDevInfo);

	return 0;
}

_PCI_DEVTABLE getDeviceInfo(string str)
{
	std::string a = str;
	string vendor = str.substr(8, 4);
	//size_t pos = str.find("&SUBSYS");
	//size_t pos1 = str.find("&DEV_");
	string device = str.substr(17, 4);
	//string device = str.substr(pos, pos1);
	//string result = "\"" + vendor + "\"" + "," + "\"" + device + "\"";
	string result0 = "0x" + vendor;
	string result1 = "0x" + device;
	cout << result0 + " " + result1 << endl;
	return temp = { result0, result1, NULL, NULL };
	//search(temp);
	//cout << vendor + " " + device << endl;
	//return vendor;
	//return NULL;
}

string getInfo(char* path, string s)
{
	return NULL;
}

_PCI_DEVTABLE search(_PCI_DEVTABLE &temp)
{
	for (int i = 0; i < PCI_DEVTABLE_LEN; i++)
	{
		if (temp.VenId == PciDevTable[i].VenId)
		{
			if (temp.DevId == PciDevTable[i].DevId)
			{
				//cout << "YEA!" << endl;
				cout << PciDevTable[i].ChipDesc << endl;
				return PciDevTable[i];
			}
		}
	}

	return temp;
}
