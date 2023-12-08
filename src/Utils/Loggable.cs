using Xenon.Utils;

namespace Xenon.Utils;

public class Loggable
{
    public Logger _logger;
    public Loggable()
    {
        _logger = new Logger(GetType().Name);
    }
}