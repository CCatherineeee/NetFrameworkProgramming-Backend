#pragma once
#include <ctime>
#include <string>
#include <msclr\marshal_cppstd.h>  

using namespace System;
using namespace msclr::interop;

namespace HandyShareOssStorageCLI {
	public ref class OssStorage
	{
		// TODO: 在此处为此类添加方法。
	public:
		static String^ GenerateName() {
			String^ str = "";
			time_t setTime;
			time(&setTime);
			tm* ptm = localtime(&setTime);
			std::string time = std::to_string(ptm->tm_year + 1900)
				+ std::to_string(ptm->tm_mon + 1)
				+ std::to_string(ptm->tm_mday)
				+ std::to_string(ptm->tm_hour) 
				+ std::to_string(ptm->tm_min)
				+ std::to_string(ptm->tm_sec);
			str += marshal_as<String^>(time); ;
			return str;
		}
	};
}
