

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 11:14:07 2038
 */
/* Compiler settings for HandyShareCOM.idl:
    Oicf, W1, Zp8, env=Win64 (32b run), target_arch=AMD64 8.01.0622 
    protocol : all , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __HandyShareCOM_i_h__
#define __HandyShareCOM_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IATL_FWD_DEFINED__
#define __IATL_FWD_DEFINED__
typedef interface IATL IATL;

#endif 	/* __IATL_FWD_DEFINED__ */


#ifndef __ATL_FWD_DEFINED__
#define __ATL_FWD_DEFINED__

#ifdef __cplusplus
typedef class ATL ATL;
#else
typedef struct ATL ATL;
#endif /* __cplusplus */

#endif 	/* __ATL_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "shobjidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IATL_INTERFACE_DEFINED__
#define __IATL_INTERFACE_DEFINED__

/* interface IATL */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IATL;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("8918365d-a7bf-4911-ac0c-d297f5012d21")
    IATL : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE Number( 
            /* [in] */ int __t,
            /* [retval][out] */ int *__result) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IATLVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IATL * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IATL * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IATL * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IATL * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IATL * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IATL * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IATL * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *Number )( 
            IATL * This,
            /* [in] */ int __t,
            /* [retval][out] */ int *__result);
        
        END_INTERFACE
    } IATLVtbl;

    interface IATL
    {
        CONST_VTBL struct IATLVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IATL_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IATL_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IATL_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IATL_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IATL_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IATL_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IATL_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IATL_Number(This,__t,__result)	\
    ( (This)->lpVtbl -> Number(This,__t,__result) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IATL_INTERFACE_DEFINED__ */



#ifndef __HandyShareCOMLib_LIBRARY_DEFINED__
#define __HandyShareCOMLib_LIBRARY_DEFINED__

/* library HandyShareCOMLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_HandyShareCOMLib;

EXTERN_C const CLSID CLSID_ATL;

#ifdef __cplusplus

class DECLSPEC_UUID("97ae114f-bb76-4f79-afcc-1f877137fe52")
ATL;
#endif
#endif /* __HandyShareCOMLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


