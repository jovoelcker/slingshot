# Slingshot-Simulator (built in Unity 5.1)

This simple project allows a player to shoot stones with a slingshot.

The technologies used in this project are:
- 2x Myo Armband (left forearm + right upperarm)
- 1x Oculus Rift
- 1x Wireless Mouse

You can just drop the Player-Prefab into your scene and you can rotate and shoot your targets.
If the Myos are on the wrong side, just press L or R to switch the sides.

Also remind, that the Myos have to look towards your wrists with their USB ports.

You may have to adjust some settings for the correct behaviour of your left arm so you can press + (in the Input settings it is =) or -
for switching the direction of movement and if your left arms roll is mapped instead of the pitch, you may need to switch axes by pressing X or Z.

After you fixed that, you can either get into T-Pose or put both arms straight forward and then press the right mouse key to calibrate your body.

Snap the imaginary sling with the left mouse key and just release it. If you do everything right, the scripts will spawn a stone,
that smashes every Rigid Body and opens the Hit-method for every object with trigger-collider.

You can enable the support for Oculus other HMDs in the player settings, to run with Oculus, you need to open the command line:
./slingshot.exe -vrmode oculus

Have Fun!!!