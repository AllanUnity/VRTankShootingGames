using System;
using System.Collections;
using UnityEngine;
using ZXing;
using ZXing.QrCode;

/// <summary>二维码</summary>
public class CSQRCoderManager : CSMonoSingleton<CSQRCoderManager>
{
    #region 扫描二维码
    public event Action<string> OnDecoderMessage;

    private Camera renderCamera;
    private WebCamTexture webCamTexture;
    private MeshRenderer meshRenderer;
    /// <summary>设备方向</summary>
    private DeviceOrientation lastDeviceOrientation;


    public void StartDecode(Action<string> action)
    {
        OnDecoderMessage -= action;
        OnDecoderMessage += action;
        lastDeviceOrientation = Input.deviceOrientation;
        InitDecode();
        meshRenderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();
        StartCoroutine(DecodeQR());
    }

    private void InitDecode()
    {
        renderCamera = GetComponentInChildren<Camera>();

        int webcamDeviceIndex = -1;
        for (int i = 0; i < webCamTexture.deviceName.Length; i++)
        {
            if (!WebCamTexture.devices[i].isFrontFacing)//可以使用的设备列表的方向与当前方向相同
            {
                webcamDeviceIndex = i;
                break;
            }
        }
        if (webcamDeviceIndex == -1 && WebCamTexture.devices.Length > 0)//如果没找到与当前方向相同的摄像机, 就使用第一个
        {
            webcamDeviceIndex = 0;
        }
        Vector3[] camfov = GetCameraForPositionByDistance(renderCamera, renderCamera.transform.localPosition.y);
        float w = Vector3.Distance(camfov[1], camfov[0]);
        float h = Vector3.Distance(camfov[2], camfov[0]);

        if (Screen.width >= Screen.height)
        {
            h = w;
        }
        else
        {
            w = h;
        }
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.transform.localScale = new Vector3(w / 10, 1, h / 10);

        webCamTexture = new WebCamTexture(WebCamTexture.devices[webcamDeviceIndex].name, 200, 200);//可调高,性能越高,分辨率越高
    }
    /// <summary>获取指定距离下相机视口的四个角的坐标</summary>
    /// <param name="cam"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    private Vector3[] GetCameraForPositionByDistance(Camera cam, float distance)
    {
        Vector3[] corners = new Vector3[4];
        Array.Resize(ref corners, 4);
        //Top left
        corners[0] = cam.ViewportToScreenPoint(new Vector3(0, 1, distance));
        //Top right
        corners[1] = cam.ViewportToScreenPoint(new Vector3(0, 1, distance));
        //Bottom left
        corners[2] = cam.ViewportToScreenPoint(new Vector3(0, 1, distance));
        //Bottom right
        corners[3] = cam.ViewportToScreenPoint(new Vector3(0, 1, distance));
        return corners;
    }

    /// <summary>识别二维码携程</summary>
    /// <returns></returns>
    private IEnumerator DecodeQR()
    {
        /*
         延时1秒, try识别, 如果摄像机方向偏离了就停止, 不识别了,否则就再次递归进入本方法
         */
        yield return new WaitForSeconds(1f);
        try
        {
            var barcoderReader = new BarcodeReader { AutoRotate = true, TryHarder = true };

            Result result = barcoderReader.Decode(webCamTexture.GetPixels32(), webCamTexture.width, webCamTexture.height);
            if (result != null)
            {
                onDecodermessage(result.Text);
                yield break;
            }
        }
        catch { }
        if (Input.deviceOrientation != lastDeviceOrientation)
        {

            ReInitRect();
        }
        else
        {
            if (webCamTexture.isPlaying)
            {
                StartCoroutine(DecodeQR());
            }
        }
    }
    /// <summary>发送识别码</summary>
    /// <param name="code"></param>
    void onDecodermessage(string code)
    {
        if (OnDecoderMessage != null)
        {
            OnDecoderMessage(code);
            Debug.Log("code:" + code);
        }
    }
    /// 需要先停止, 然后才能进入开始方法, 所以不能放入到原方法中
    private void ReInitRect()
    {
        StopCoroutine(DecodeQR());
        webCamTexture.Stop();
        StartDecode(OnDecoderMessage);
    }
    #endregion
    #region 生成二维码
    /// <summary>生成二维码</summary>
    /// <param name="qrcodecontent">二维码内容</param>
    /// <returns></returns>
    public Texture2D GetQRcodeTexture(string qrcodecontent)
    {
        Texture2D texture = new Texture2D(256, 256);
        texture.SetPixels32(GetGeneQRCode(qrcodecontent, 256, 256));

        texture.Apply();
        return texture;
    }
    Color32[] GetGeneQRCode(string content, int width, int height)
    {
        QrCodeEncodingOptions options = new QrCodeEncodingOptions();
        options.CharacterSet = "UTF-8";//编码格式

        options.Width = width;
        options.Height = height;

        options.Margin = 3;//空白边

        BarcodeWriter m_barcodeWriter = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = options
        };

        return m_barcodeWriter.Write(content);

    }

    #endregion

}
