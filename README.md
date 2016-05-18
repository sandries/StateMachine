# State Machine

This solution contains a simple implementation of a state machine.

A transition is defined by : 

 * source state
 * destination state
 * action 
 * event
 * condition
 
A transition is valid and can take place when the current state of the state machine is in the **source state** of the transition and the associated event is fired.

When the transition takes place, the associated action gets executed and the current state of the state machine gets updated with the **destination state**.

### Conditions

Conditions are used to simulate an if-else statement in the state machine.

Example: 

| Transition | Source State | Destination State | Event | Condition | 
|------------|--------------|-------------------|-------|-----------|
| T1         | S1           | S2                | E1    | C1        |
| T2         | S1           | S3                | E1    | -         |


When event `E1` is fired and the current state of our state machine is `S1` and we identified these 2 possible transitions( `T1` and `T2`) we first check if the transition that has a condition associated is possible.

If condition `C1` is satisfied, then the state machine moves to `S2`, if not, then it moves to `S3` ( transition `T2` takes place).

In the Atm example : 

| Transition | Source State | Destination State   | Event | Condition | 
|------------|--------------|---------------------|-------|-----------|
| T1         | EnteredMenu  | WithdrawedAmount    | WithdrawAmount    | AccountHasSufficientAmount        |
| T2         | EnteredMenu  | Insufficient Amount | WithdrawAmount    | -         |


Note : The solution contains a simple ATM console project to test the implementation.

 
