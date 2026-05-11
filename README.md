# Blast of Undying (Artificer Spear Deflection)

Makes Artificer automatically deflect incoming scavenger spears by consuming half her normal Explosion Capacity.

(My first mod)

![Artijak](/assets/thumbnail.png)

Tired of getting randomly sniped by an offscreen scavenger?
Want to jump into a scavenger pack without instantly dying before you can even react?

With this mod, if Artificer still has enough Explosion Capacity left, an incoming scavenger spear is deflected instead of killing her.

The deflection uses the same explosion resource as Artificer’s normal explosive abilities, so you will need to carefully manage that resource!

Be careful, though:
If she is already overheated, she can EXPLODE! (Can be toggled off) 

## Notes

* Currently only affects scavenger spears
* Other projectile support could be added later

## Feedback

Bug reports and suggestions are very welcome.
Please post them in the Workshop comments or on the GitHub issue tracker.

Have fun causing havoc. >:}

## Technical overview

* Vanilla Artificer Explosion Capacity: `10`
* Default Deflection Cost: `5`
* Default Stun Threshold: `7`
* Default Stun Duration (frames): `60` (1 second)
* Default Explode When Can't Reflect: `true`

By default, reaching `10` will kill Artificer from overheating, and reaching `7` will stun her.

If you disable "Explode When Can't Reflect" and deflection overheats Artificer past the stun threshold, Artificer will be stunned, but won't cause PyroDeath. (Explode)

### Examples

1. `counter = 0`
   After deflection: `counter = 5`

2. `counter = 2`
   After deflection: `counter = 7` -> Artificer is stunned for x1 duration

3. `counter = 6`
   After deflection: `counter = 9` -> Artificer is stunned for x3 duration, but death is prevented

4. `counter = 7`
   No deflection: the spear hits normally, as Artificer is stunned
