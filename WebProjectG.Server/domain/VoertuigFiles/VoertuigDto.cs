namespace WebProjectG.Server.domain.VoertuigFiles
{
    public record class GetVoertuigenDto
    {
        public DateOnly? StartDatum { get; set; }
        public DateOnly? EindDatum { get; set; }

        public decimal? MinPrijs { get; set; } = null;
        public decimal? MaxPrijs { get; set; } = null;
    }
}
