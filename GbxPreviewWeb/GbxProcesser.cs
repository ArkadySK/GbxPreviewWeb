using GBX.NET;
using GBX.NET.Engines.Game;

namespace GbxPreviewWeb
{
    public class GbxProcesser
    {
        

        public CGameCtnChallenge? ProcessMap(string path)
        {
            if (string.IsNullOrEmpty(path)) return null;
            Node? map = GameBox.ParseNode(path);
            if(map is CGameCtnChallenge challenge)
                return challenge;
            else return null;
        }
    }
}
