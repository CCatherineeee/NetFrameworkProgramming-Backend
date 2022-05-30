// dllmain.h: 模块类的声明。

class CHandyShareCOMModule : public ATL::CAtlDllModuleT< CHandyShareCOMModule >
{
public :
	DECLARE_LIBID(LIBID_HandyShareCOMLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_HANDYSHARECOM, "{b2f357ef-a80b-4498-b801-7352b9245795}")
};

extern class CHandyShareCOMModule _AtlModule;
