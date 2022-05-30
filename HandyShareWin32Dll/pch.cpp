// pch.cpp: 与预编译标头对应的源文件

#include "pch.h"
#include <time.h>
#include<stdlib.h>


EXTERN_C _declspec(dllexport)int Add(int x, int y) {
	return x + y;
}

EXTERN_C _declspec(dllexport)int Subtract(int x, int y) {
	return x - y;
}
// 当使用预编译的头时，需要使用此源文件，编译才能成功。
