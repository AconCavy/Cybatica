namespace Cybatica.Empatica
{
    public interface IEmpaticaModel
    {
        Acceleration Acceleration { get; }
        BatteryLevel BatteryLevel { get; }
        Bvp Bvp { get; }
        Gsr Gsr { get; }
        Hr Hr { get; }
        Ibi Ibi { get; }
        Tag Tag { get; }
        Temperature Temperature { get; }
    }
}