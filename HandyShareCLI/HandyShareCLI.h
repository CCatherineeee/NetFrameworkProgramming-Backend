#pragma once
#include <string.h>
#include <time.h>
#include <stdlib.h>

using namespace System;

namespace HandyShareCLI {
	public ref class VerfifyHandler
	{
		// TODO: 在此处为此类添加方法。
	public:
		static String^ generate_code() {
			String^ str="";
			int n = 5;
			int i, j, len;
			char pstr[] = "23456789abcdefghjkmnpqrstuvwxyzABCDEFGHIJLMNOPQRSTUVWXYZ";
			len = strlen(pstr);         //求字符串pstr的长度
			srand(time(0));
			for (i = 0; i < n; i++) {
				j = rand() % len;        //生成0~len-1的随机数
				str += pstr[j];
			}
			return str;
		}
	};
}
