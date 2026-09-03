namespace Test_Taste_Console_Application.Constants
{
    public static class UriPath
    {
        public const string BaseUri = "https://api.le-systeme-solaire.net";
        private const string BodiesUri = "/rest/bodies";

        // avgTemp is the bodies API mean temperature in Kelvin. It must be requested
        // explicitly when using the data= filter, otherwise the field is omitted.
        public const string GetAllPlanetsWithMoonsQueryParameters =
            BodiesUri + "?data=id,semiMajorAxis,moons,moon,rel,avgTemp&filter[]=isPlanet,neq,false";

        public const string GetAllMoonsWithMassQueryParameters = BodiesUri +
                                               "?data=id,mass,massValue,massExponent,massValue,avgTemp&filter[]=aroundPlanet,gt,null";

        public const string GetMoonByIdQueryParameters = BodiesUri + "/";
        public const string GetMoonByIdDataQueryParameters = "?data=id,avgTemp,mass,massValue,massExponent";
    }
}
