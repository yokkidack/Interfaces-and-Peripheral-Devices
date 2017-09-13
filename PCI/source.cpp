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
using namespace std;

string getDeviceInfo(string);

int main(int argc, char *argv[])
{
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
			&data,
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


	getDeviceInfo(devices.front());
	SetupDiDestroyDeviceInfoList(hDevInfo);

	return 0;
}

string getDeviceInfo(string str)
{
	string vendor = str.substr(4, 8);
	string device = str.substr(13, 8);
	string result = "\"" + vendor + "\"" + "," + "\"" + device + "\"";
	//cout << vendor << " " << device << endl;
	cout << result << endl;
	return result;
}
