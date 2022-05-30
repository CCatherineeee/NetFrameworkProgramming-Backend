// ATL.cpp: CATL 的实现

#include "pch.h"
#include "ATL.h"
#include <time.h>


// CATL

STDMETHODIMP CATL::Number(int __t, int* __result) {
	time_t cur;
	int t = (int)time(&cur);
	*__result = rand() % 100 + 900 + t * 1000;
	return S_OK;
}
