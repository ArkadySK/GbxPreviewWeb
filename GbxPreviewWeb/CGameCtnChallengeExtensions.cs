using GBX.NET.Engines.Game;

namespace GbxPreviewWeb
{
    public static class CGameCtnChallengeExtensions
    {
       

        public static string GetMapMood(this CGameCtnChallenge Map)
        {
            if (Map == null) return "Unknown";
            if (Map.Decoration == null) return "Unknown";
            var Moods = new string[]{"Sunrise", "Day" , "Sunset", "Night"};

            foreach (var Mood in Moods)
            {
                if(Map.Decoration.Id.Contains(Mood))
                    return Mood;
            }

            return "Unknown";
        }
    }
}
