using Microsoft.Xna.Framework.Audio;

namespace PowerPlant.Core.Content;

public class AudioPlayer
{
    public static void PlaySoundEffect(SoundEffect soundEffect)
    {
        soundEffect.Play();
    }

    public static void PlayMusic(SoundEffect soundEffect, bool loopMusic)
    {
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
        soundEffectInstance.IsLooped = loopMusic;
        soundEffectInstance.Play();
    }
}
