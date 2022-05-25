// pch.cpp: 与预编译标头对应的源文件

#include "pch.h"
#include <time.h>
#include<stdlib.h>


char* create() {
	char str[5];
	int n = 5;
	int i, j, len;
	char pstr[] = "23456789abcdefghjkmnpqrstuvwxyzABCDEFGHIJLMNOPQRSTUVWXYZ";
	len = strlen(pstr);         //求字符串pstr的长度
	srand(time(0));
	for (i = 0; i < n; i++) {
		j = rand() % len;        //生成0~len-1的随机数
		str[i] = pstr[j];
	}
	return str;
}
// 当使用预编译的头时，需要使用此源文件，编译才能成功。
