namespace Xenon.Communication.Messages.Outgoing;

using System.Text;

public class OutgoingMessage
{
    private byte[] message;
    public short Header;
    private uint index;

    public OutgoingMessage(short header)
    {
        Header = header;
        message = Array.Empty<byte>();
        index = 0;

        WriteShort(header);
    }

    public void WriteByte(byte value)
    {
        Array.Resize(ref message, message.Length + 1);
        message[^1] = value;
        index++;
    }

    public void WriteBool(bool value)
    {
        if (value)
            WriteByte(1);
        else
            WriteByte(0);
    }

    public void WriteShort(short value)
    {
        byte[] bytes = new byte[2];
        bytes[0] = (byte)((value >> 8) & 0xFF);
        bytes[1] = (byte)(value & 0xFF);
        AppendBytes(bytes);
    }

    public void WriteInt(object value)
    {
        int newValue = Convert.ToInt32(value);

        byte[] bytes = new byte[4];
        bytes[0] = (byte)((newValue >> 24) & 0xFF);
        bytes[1] = (byte)((newValue >> 16) & 0xFF);
        bytes[2] = (byte)((newValue >> 8) & 0xFF);
        bytes[3] = (byte)(newValue & 0xFF);
        AppendBytes(bytes);
    }

    public void WriteString(string data)
    {
        byte[] dataBytes = Encoding.Default.GetBytes(data);
        WriteShort((short)dataBytes.Length);
        AppendBytes(dataBytes);
    }

    public byte[] Compose()
    {
        byte[] lengthBytes = new byte[4];
        lengthBytes[0] = (byte)((index >> 24) & 0xFF);
        lengthBytes[1] = (byte)((index >> 16) & 0xFF);
        lengthBytes[2] = (byte)((index >> 8) & 0xFF);
        lengthBytes[3] = (byte)(index & 0xFF);

        byte[] result = new byte[lengthBytes.Length + message.Length];
        Array.Copy(lengthBytes, result, lengthBytes.Length);
        Array.Copy(message, 0, result, lengthBytes.Length, message.Length);

        return result;
    }

    private void AppendBytes(byte[] bytes)
    {
        List<byte> byteArray = new();

        byteArray.AddRange(bytes);

        message = byteArray.ToArray();
        index += (uint)bytes.Length;
    }
}