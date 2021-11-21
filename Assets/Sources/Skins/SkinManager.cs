using Sources.Identification;
using Sources.Registration;

namespace Sources.Skins {
    public class SkinManager : Manager<Skin> {
        public SkinManager() : base(Identifiers.ManagerSkin) {
            Register(new Skin(Identifiers.SkinDefault, "Default", "Skins/Player"));
            Register(new Skin(Identifiers.SkinBlue, "Ice Block", "Skins/BlueSkin"));
            Register(new Skin(Identifiers.SkinBlue, "Ice Block", "Skins/BlueSkin"));
            Register(new Skin(Identifiers.SkinGray, "Noir Block", "Skins/GraySkin"));
            Register(new Skin(Identifiers.SkinGreen, "Line Flavour", "Skins/GreenSkin"));
            Register(new Skin(Identifiers.SkinPink, "Candy", "Skins/PinkSkin"));
            Register(new Skin(Identifiers.SkinPurple, "Blackberry Jam", "Skins/PurpleSkin"));
            Register(new Skin(Identifiers.SkinRed, "Hell Block", "Skins/RedSkin"));
        }
    }
}