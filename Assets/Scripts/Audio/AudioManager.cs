using UnityEngine;


namespace AudioHelper
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;

        public AudioSource PickUpSource;
        public AudioSource MiscSource;
        public AudioSource MainSource;


        public float lowPitchRange = .95f;
        public float highPitchRange = 1.05f;


        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            //DontDestroyOnLoad(gameObject);
        }


        public void RandomizePickUpSfx(params AudioClip[] clips)
        {
            if (PickUpSource.enabled == true)
            {
                //Generate a random number between 0 and the length of our array of clips passed in.
                int randomIndex = Random.Range(0, clips.Length);

                //Choose a random pitch to play back our clip at between our high and low pitch ranges.
                float randomPitch = Random.Range(lowPitchRange, highPitchRange);

                //Set the pitch of the audio source to the randomly chosen pitch.
                PickUpSource.pitch = randomPitch;

                //Set the clip to the clip at our randomly chosen index.
                PickUpSource.clip = clips[randomIndex];

                //Play the clip.
                PickUpSource.Play();
            }
        }

        public void RandomizeMiscSfx(params AudioClip[] clips)
        {

            if (MiscSource.enabled == true)
            {
                //Generate a random number between 0 and the length of our array of clips passed in.
                int randomIndex = Random.Range(0, clips.Length);

                //Choose a random pitch to play back our clip at between our high and low pitch ranges.
                float randomPitch = Random.Range(lowPitchRange, highPitchRange);

                //Set the pitch of the audio source to the randomly chosen pitch.
                MiscSource.pitch = randomPitch;

                //Set the clip to the clip at our randomly chosen index.
                MiscSource.clip = clips[randomIndex];

                //Play the clip.
                MiscSource.Play();
            }
        }

        public void StopMainSound()
        {
            MainSource.Stop();
        }


        public void DestroyAudioManager()
        {
            Destroy(gameObject);
        }
    }
}