using Library.Helpers;
using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using System.Collections.Generic;
using System.Windows.Input;

namespace Library.ViewModel
{
    public class PaletteViewModel : ObservableObject
    {
        public IEnumerable<Swatch> Swatches { get; set; }

        public PaletteViewModel()
        {
            Swatches = new SwatchesProvider().Swatches;
        }

        public ICommand SetThemeCommand
        {
            get
            {
                return new DelegateCommand(o => changeTheme((bool)o));
            }
        }

        private void changeTheme(bool isDark)
        {
            new PaletteHelper().SetLightDark(isDark);
        }

        public ICommand ApplyPrimaryCommand { get; } = new DelegateCommand(o => applyPrimary((Swatch)o));

        public ICommand ApplyAccentCommand { get; } = new DelegateCommand(o => applyAccent((Swatch)o));

        private static void applyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        private static void applyAccent(Swatch swatch)
        {
            new PaletteHelper().ReplaceAccentColor(swatch);
        }

    }
}
