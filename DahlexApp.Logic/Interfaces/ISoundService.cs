using Plugin.Maui.Audio;

namespace DahlexApp.Views.Board
{
    public interface ISoundService
    {
        void PlayBomb();
        void PlayTele();
        void PlayCrash();
    }

}
