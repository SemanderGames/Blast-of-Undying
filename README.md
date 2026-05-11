# Blast of Undying — Artificer Spear Deflection

Makes Artificer automatically deflect incoming scavenger spears by consuming her normal Explosion Capacity.

Tired of getting randomly sniped by an offscreen scavenger?
Want to jump into a scavenger pack without instantly dying before you can even react?

With this mod, if Artificer still has enough Explosion Capacity left, an incoming scavenger spear is deflected instead of killing her.

The deflection uses the same explosion resource as Artificer’s normal explosive abilities, so it fits into her existing risk/reward gameplay instead of adding a separate shield meter.

Be careful, though:
if your counter gets pushed too high, Artificer gets stunned and can no longer deflect more spears until she recovers.
If she is already too overheated, the spear hits normally.

## Notes

* Currently only affects scavenger spears
* Other projectile support could be added later

## Technical explanation

Vanilla Artificer Explosion Capacity: `10`
Default Deflection Cost: `4`
Default Stun Threshold: `7`

If a deflection pushes `pyroJumpCounter` to or past the stun threshold, Artificer is stunned, but the mod prevents that deflection itself from causing PyroDeath.

### Examples

1. `counter = 0`
   After deflection: `counter = 4`

2. `counter = 3`
   After deflection: `counter = 7` → Artificer is stunned

3. `counter = 6`
   After deflection: `counter = 9` → Artificer is stunned

4. `counter = 7`
   No deflection: the spear hits normally

## Feedback

Bug reports and suggestions are very welcome.
Please post them in the Workshop comments or on the GitHub issue tracker.

Have fun causing havoc. >:}

