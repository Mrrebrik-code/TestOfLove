using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMono<AudioManager>
{
	[SerializeField] private AudioSource _soundSource;
	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _sfxSource;

	[SerializeField] private List<Audio> _soundsAudio = new List<Audio>();
	private Dictionary<Sounds, AudioClip> _clipSounds = new Dictionary<Sounds,AudioClip>();

	public override void Awake()
	{
		base.Awake();
		Init();
	}

	private void Init()
	{
		_soundsAudio.ForEach(audio =>
		{
			_clipSounds.Add(audio.Type, audio.Clip);
		});
	}

	public void PlaySound(Sounds type)
	{
		var clip = _clipSounds[type];
		_soundSource.PlayOneShot(clip);
	}

	[System.Serializable]
	public class Audio
	{
		[SerializeField] private Sounds _type;
		[SerializeField] private AudioClip _clip;

		public Sounds Type { get { return _type; } }
		public AudioClip Clip { get { return _clip;} }
	}
}
