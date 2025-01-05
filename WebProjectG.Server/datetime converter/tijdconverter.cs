using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace WebProjectG.Server.datetime_converter
{
    public class TijdConverter : ValueConverter<DateOnly, DateTime>
    {
        public TijdConverter() : base(dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), dateTime => DateOnly.FromDateTime(dateTime))
            { }
    }
}
