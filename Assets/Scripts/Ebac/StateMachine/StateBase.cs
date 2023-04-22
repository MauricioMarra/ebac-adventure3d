using UnityEngine;

public class StateBase
{
    public string message;

    virtual public void OnStateEnter()
    {
        Debug.Log(message);
    }

    virtual public void OnStateExit()
    {
        Debug.Log(message);
    }
}

public class StateRunning : StateBase
{
    public override void OnStateEnter()
    {
        message = $"Entering {this.GetType()}";

        base.OnStateEnter();

        Time.timeScale = 1.0f;
    }

    public override void OnStateExit()
    {
        message = $"Leaving {this.GetType()}";

        base.OnStateExit();
    }
}

public class StatePaused : StateBase
{
    public override void OnStateEnter()
    {
        message = $"Entering {this.GetType()}";

        base.OnStateEnter();

        Time.timeScale = 0.0f;
    }

    public override void OnStateExit()
    {
        message = $"Leaving {this.GetType()}";

        base.OnStateExit();
    }
}

public class StateDeath: StateBase
{
    public override void OnStateEnter()
    {
        message = $"Entering {this.GetType()}";

        base.OnStateEnter();
    }

    public override void OnStateExit()
    {
        message = $"Leaving {this.GetType()}";

        base.OnStateExit();
    }
}

public class StateEndGame: StateBase
{
    public override void OnStateEnter()
    {
        message = $"Entering {this.GetType()}";

        base.OnStateEnter();

        Time.timeScale = 0.0f;
    }

    public override void OnStateExit()
    {
        message = $"Leaving {this.GetType()}";

        base.OnStateExit();
    }
}