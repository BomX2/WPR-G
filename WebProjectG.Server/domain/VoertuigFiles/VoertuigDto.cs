namespace WebProjectG.Server.domain.VoertuigFiles
{
    public record class GetVoertuigenDto
    {
        public DateOnly? StartDatum { get; set; }
        public String? OphaalTijd { get; set; }
        public DateOnly? EindDatum { get; set; }
        public String? InleverTijd { get; set; }
        public String? Soort { get; set; }

    }
}
