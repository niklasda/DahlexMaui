using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahlexApp.Views.Board
{
    public class AudioPlayer
    {
        // TODO  use this to prevent parallell play
        public AudioPlayer( IAudioManager audio)
        {
            _audio = audio;

        }

        private readonly IAudioManager _audio;


        public async Task PlayBomb()
        {
            // ImageSource.FromFile();

            var stream = await FileSystem.OpenAppPackageFileAsync("bomb.wav");
            var audioPlayer = _audio.CreatePlayer(stream);

            audioPlayer.Play();

            //ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            //  _audio.Load(GetStreamFromFile("bomb.wav"));
            //player.Play();
        }

        public async Task PlayTele()
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("tele.wav");
            using (IAudioPlayer audioPlayer = _audio.CreatePlayer(stream))
            {
                audioPlayer.Play();
            }
            //ISimpleAudioPlayer player = CrossSimpleAudioPlayer.Current;
            //  player.Load(GetStreamFromFile("tele.wav"));
            //player.Play();
        }

        public async Task PlayCrash()
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("heap.wav");
            var audioPlayer = _audio.CreatePlayer(stream);
            audioPlayer.Play();

            //        IAudioManager player = CrossSimpleAudioPlayer.Current;
            // var v = player.Volume;
            //          player.Load(GetStreamFromFile("heap.wav"));
            //        player.Play();
        }
    }
}
