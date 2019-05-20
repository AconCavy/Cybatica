using Cybatica.Empatica;

namespace Cybatica.Services
{
    public interface ICybaticaHandler
    {
        Acceleration GetAcceleration();

        BatteryLevel GetBatteryLevel();

        Bvp GetBvp();

        Gsr GetGsr();

        Hr GetHr();

        Ibi GetIbi();

        Tag GetTag();

        Temperature GetTemperature();

        EmpaticaDeviceStatus GetDeviceStatus();

        EmpaticaSensorStatus GetEmpaticaSensorStatus();

        EmpaticaBLEStatus GetEmpaticaBLEStatus();

        float GetCybersickness();

        float GetNnmean();

        float GetSdnn();

        float GetRmssd();

        float GetPpSd1();

        float GetPpSd2();

        float GetScr();
    }
}
