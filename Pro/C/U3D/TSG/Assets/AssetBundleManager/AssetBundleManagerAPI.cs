﻿using System;

namespace AssetBundles
{
  public class AssetBundleManagerAPI : IAssetBundleManagerAPI
  {
    static AssetBundleManagerAPI()
    {
      AssetBundleManager.Init();
    }
    
    void IAssetBundleManagerAPI.Init() { }


    void IAssetBundleManagerAPI.LoadAssetAsync<TAsset>(
      string assetBundleName,
      string assetName,
      Action<TAsset> onLoad)
    {
      AssetBundleManager.instance.LoadAssetAsync<TAsset>(
        assetBundleName, assetName, onLoad);
    }
  }
}
