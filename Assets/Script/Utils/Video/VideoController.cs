using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField]
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;

        StartCoroutine(WaitForShowDialog());
    }

    private IEnumerator WaitForShowDialog()
    {
        yield return new WaitForEndOfFrame();

        DialogManager.Singleton.StartDialog(EDialogName.Level0_Vision_Start);
        StartCoroutine(WaitUntilDialogIsOpen(true));
    }

    private IEnumerator WaitUntilDialogIsOpen(bool isStartingVideo)
    {
        yield return new WaitUntil(() => !DialogManager.Singleton.IsDialogOpen());

        if (isStartingVideo)
        {
            if (!videoPlayer.isPlaying)
                videoPlayer.Play();
        }
        else
        {
            GameManager.Singleton.NextLevel();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        DialogManager.Singleton.StartDialog(EDialogName.Level0_Vision_End);
        StartCoroutine(WaitUntilDialogIsOpen(false));
    }
}
