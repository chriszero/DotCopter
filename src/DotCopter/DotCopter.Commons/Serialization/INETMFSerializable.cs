namespace DotCopter.Commons.Serialization
{
    public interface INETMFSerializable //:  where T : new() in classic C# this is what i would do force a parameterless contructor, cant do thisin netmf
    {
        byte[] Serialize();
        object Deserialize(byte[] buffer);
    }
}
