using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVariableManager {

    private static GameVariableManager inst;

    public static Inst() {
        if (inst == null) {
            inst = new GameVariableManager();
        }

        return inst;
    }

    private IDictionary<string, GameVar> vars = new Dictionary();

    public GameVar getVar(string name) {
        return vars.TryGetValue(name);
    }

    public void setNewVar(string name, GameVar var) {
        vars.Add(name, var);
    }

}

public class GameVar<T> {
    
    T data;

    public T read() {
        return data;
    }

    public void write(T newData) {
        data = newData;
    }

}
