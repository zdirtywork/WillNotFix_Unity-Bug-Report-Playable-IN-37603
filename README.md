# [Avoidable][Won't Fix] Unity-Bug-Report-Playable-IN-37603

**Unity has stated that they will not fix this bug.**

> RESOLUTION NOTE:
> This issue happens because the PlayableGraph with a TimeUpdateMode set to Manual does not schedule any graph playback when calling `PlayableGraph.Play` but is still in 'IsPlaying' mode.
> 
> **Calling `PlayableGraph.Stop` before changing TimeUpdateMode to anything else than Manual will ensure that the graph is schedule the next time `PlayableGraph.Play` is called.**
> 
> Changing this behaviour for PlayableGraph breaks too many existing behaviours, and it's not possible to change it at this point.

## About this issue

When the PlayableGraph is in `Manual` update mode and playing, changing its update mode to `non-Manual` will not cause the PlayableGraph to continue running.

![Sample](./imgs~/img_sample.gif)

## How to reproduce

1. Open the "SampleScene".
2. Enter play mode, and you will see the character walking in the Game view.
3. Click the "Set to Manual" button in the Game view.
4. Click the "Play Graph" button in the Game view.
5. Click the "Set to GameTime" button in the Game view.
   
Expected result: The character continue walking.

Actual result: The character remained paused.

## Solution

Please see the [`PlayableGraphPlayingTest.SetUpdateModeToGameTime`](./Assets/PlayableGraphPlayingTest.cs) method.
