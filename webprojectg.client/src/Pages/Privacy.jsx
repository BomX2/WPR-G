
const Privacy = () => {
    return (
        <div className="privacyverklaring-pagina">
            {/* Introduction */}
            <section className="intro">
                <h2>Privacyverklaring</h2>
                <p>
                    Uw privacy is belangrijk voor ons. In deze privacyverklaring leggen wij uit welke
                    gegevens we verzamelen, waarom we deze verzamelen en hoe we hiermee omgaan.
                    Door gebruik te maken van onze diensten stemt u in met ons privacybeleid.
                </p>
            </section>

            {/* Data We Collect */}
            <section className="data-collection">
                <h2>Welke Gegevens Verzamelen Wij?</h2>
                <ul>
                    <li>Naam en contactgegevens (e-mailadres, telefoonnummer, adres)</li>
                    <li>Gegevens over geboekte voertuigen en huurperiodes</li>
                    <li>Betalingsinformatie (indien van toepassing)</li>
                    <li>IP-adres en apparaatgegevens bij gebruik van onze website</li>
                </ul>
            </section>

            {/* Purpose of Data Collection */}
            <section className="data-purpose">
                <h2>Waarom Verzamelen Wij Gegevens?</h2>
                <ul>
                    <li>Om reserveringen en huurovereenkomsten te beheren</li>
                    <li>Om onze klantenservice te verbeteren</li>
                    <li>Om wettelijke verplichtingen na te komen</li>
                    <li>Om u op de hoogte te houden van aanbiedingen en updates (indien u zich heeft ingeschreven voor onze nieuwsbrief)</li>
                </ul>
            </section>

            {/* Data Sharing */}
            <section className="data-sharing">
                <h2>Met Wie Delen Wij Uw Gegevens?</h2>
                <p>
                    Wij delen uw gegevens alleen wanneer dit noodzakelijk is, zoals:
                </p>
                <ul>
                    <li>Met betalingsverwerkers voor het afhandelen van betalingen</li>
                    <li>Met derden voor wettelijke verplichtingen of juridische processen</li>
                    <li>Met dienstverleners die ons ondersteunen in het aanbieden van onze diensten</li>
                </ul>
            </section>

            {/* Data Protection */}
            <section className="data-protection">
                <h2>Hoe Beschermen Wij Uw Gegevens?</h2>
                <p>
                    Wij nemen passende beveiligingsmaatregelen om uw gegevens te beschermen tegen
                    ongeoorloofde toegang, verlies of diefstal. Uw gegevens worden veilig opgeslagen
                    en alleen toegankelijk voor geautoriseerd personeel.
                </p>
            </section>

            {/* Your Rights */}
            <section className="your-rights">
                <h2>Uw Rechten</h2>
                <p>U heeft de volgende rechten met betrekking tot uw persoonlijke gegevens:</p>
                <ul>
                    <li>Inzage in uw gegevens</li>
                    <li>Correctie of verwijdering van uw gegevens</li>
                    <li>Beperking van de verwerking van uw gegevens</li>
                    <li>Bezwaar maken tegen de verwerking van uw gegevens</li>
                    <li>Overdracht van uw gegevens (dataportabiliteit)</li>
                </ul>
                <p>
                    Neem contact met ons op via de onderstaande gegevens om uw rechten uit te oefenen.
                </p>
            </section>
        </div>
    );
};

export default Privacy;