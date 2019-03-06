using UnityEngine;

/// <summary>声音监控脚本</summary>
public class CSAudioManager : MonoBehaviour
{
    public AudioSource audioL;
    private const int timeLimit = 3600;

    string selectedDevice = null;

    /// <summary>声音音量</summary>
    public float Volume
    {
        get
        {
            if (Microphone.IsRecording(null))
            {
                int sampleSize = 128;
                float[] samples = new float[sampleSize];
                int startPostion = Microphone.GetPosition(selectedDevice) - (sampleSize + 1);
                audioL.clip.GetData(samples, startPostion);
                float levelMax = 0;
                for (int i = 0; i < sampleSize; ++i)
                {
                    float wavePeak = samples[i];
                    if (levelMax < wavePeak)
                        levelMax = wavePeak;
                }
                return levelMax * 100;
            }
            return 0;
        }
    }

    /// <summary>开始监听</summary>
    private void StartRecord()
    {
        audioL.Stop();
        audioL.loop = false;
        audioL.mute = false;
        audioL.clip = Microphone.Start(selectedDevice, true, timeLimit, 128);
    }
    /// <summary>停止监听</summary>
    public void StopRecord()
    {
        Microphone.End(selectedDevice);
        audioL.Stop();
    }
}
