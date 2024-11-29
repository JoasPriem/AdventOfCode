using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CubeGameVerifier.GameVerifier
{
    public class SeedLocationMapper
    {
        private readonly string _filePath;
        private List<string> _seeds;
        private List<List<string>> _SeedsToSoilMap;
        private List<List<string>> _SoilToFertilizerMap;
        private List<List<string>> _FertilizerToWaterMap;
        private List<List<string>> _WaterToLightMap;
        private List<List<string>> _LightToTempMap;
        private List<List<string>> _TempToHumidityMap;
        private List<List<string>> _HumidityToLocationMap;

        public SeedLocationMapper(string filePath)
        {
            this._filePath = filePath;
            InitializeMappings();
        }

        private void InitializeMappings()
        {
            string inputFileAsString = string.Empty;
            StreamReader sr = new StreamReader(_filePath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                inputFileAsString += line;
            }
            var dict = GetMappingStrings(inputFileAsString);
            _seeds = dict[MapNames.Seeds].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            _SeedsToSoilMap = dict[MapNames.SeedToSoil].Split()
        }

        private enum MapNames
        {
            Seeds,
            SeedToSoil,
            SoilToFertilizer,
            FertilizerToWater,
            WaterToLight,
            LightToTemp,
            TempToHumidity,
            HumidityToLocation
        }

        private Dictionary<MapNames, string> GetMappingStrings(string inputFileAsString)
        {
            string[] splittedString = inputFileAsString.Split(":");
            int indexToGrab = 1;
            Dictionary<MapNames, string> mappingDict = new Dictionary<MapNames, string>();
            foreach (MapNames mapName in Enum.GetValues(typeof(MapNames)))
            {
                mappingDict[mapName] = splittedString[indexToGrab];
                indexToGrab += 2;
            }

            return mappingDict;
        }

        public int GetLowestMappedLocation()
        {
            return 0;
        }




    }
}
