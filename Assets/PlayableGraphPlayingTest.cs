using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.UI;

// About this issue:
// 
// When the PlayableGraph is in Manual update mode and playing, changing its update mode to non-Manual will not cause the PlayableGraph to continue running.
// 
// How to reproduce:
// 1. Open the "SampleScene".
// 2. Enter play mode, and you will see the character walking in the Game view.
// 3. Click the "Set to Manual" button in the Game view.
// 4. Click the "Play Graph" button in the Game view.
// 5. Click the "Set to GameTime" button in the Game view.
//    
// Expected result: The character continue walking.
// Actual result: The character remained paused.
// 
// Solution:
// Please see the `PlayableGraphPlayingTest.SetUpdateModeToGameTime` method below.


[RequireComponent(typeof(Animator))]
public class PlayableGraphPlayingTest : MonoBehaviour
{
    public Text updateModeText;
    public Text playingText;
    public AnimationClip clip;

    private PlayableGraph _graph;

    private void Awake()
    {
        _graph = PlayableGraph.Create("PlayableGraph Playing Test");
        _graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);

        // _clipPlayable -> output
        var clipPlayable = AnimationClipPlayable.Create(_graph, clip);
        var output = AnimationPlayableOutput.Create(_graph, "Anim Output", GetComponent<Animator>());
        output.SetSourcePlayable(clipPlayable);

        _graph.Play();
    }

    private void LateUpdate()
    {
        updateModeText.text = $"UpdateMode: {_graph.GetTimeUpdateMode()}";
        playingText.text = $"IsPlaying: {_graph.IsPlaying()}";
    }

    private void OnDestroy()
    {
        _graph.Destroy();
    }

    public void SetUpdateModeToManual()
    {
        _graph.SetTimeUpdateMode(DirectorUpdateMode.Manual);
    }

    public void SetUpdateModeToGameTime(bool temporarilyFix)
    {
        if (!temporarilyFix)
        {
            _graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
            return;
        }

        // Temporarily fix
        var isPlaying = _graph.IsPlaying();
        _graph.Stop();
        _graph.SetTimeUpdateMode(DirectorUpdateMode.GameTime);
        if (isPlaying)
        {
            _graph.Play();
        }
    }

    public void PlayGraph()
    {
        _graph.Play();
    }

    public void StopGraph()
    {
        _graph.Stop();
    }
}