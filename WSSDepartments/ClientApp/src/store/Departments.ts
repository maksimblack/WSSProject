import { Action, Reducer } from 'redux';
import { AppThunkAction } from '.';

// -----------------
// STATE - This defines the type of data maintained in the Redux store.

export interface DepartmentsState {
    isLoading: boolean;
    companies: Company[];
}

export interface Company {
    companyId: number;
    name: string;
    description: string;
    departments: Department[]
}

export interface Department{
    departmentId: number;
    name: string;
    description: string;
    companyId: number;
    divisions: Division[]
}

export interface Division {
    divisionId: number;
    name: string;
    description: string;
    departmentId: number;
}

// -----------------
// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
// They do not themselves have any side-effects; they just describe something that is going to happen.

interface RequestDepartmentsAction {
    type: 'REQUEST_DEPARTMENTS';
}

interface ReceiveDepartmentsAction {
    type: 'RECEIVE_DEPARTMENTS';
    companies: Company[];
}


// Declare a 'discriminated union' type. This guarantees that all references to 'type' properties contain one of the
// declared type strings (and not any other arbitrary string).
type KnownAction = RequestDepartmentsAction | ReceiveDepartmentsAction ;

// ----------------
// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
// They don't directly mutate state, but they can have external side-effects (such as loading data).

export const actionCreators = {
    requestDepartments: (): AppThunkAction<KnownAction> => (dispatch, getState) => {
        // Only load data if it's something we don't already have (and are not already loading)
        const appState = getState();
        if (appState && appState.departments) {
            fetch(`company`)
                .then(response => response.json() as Promise<Company[]>)
                .then(data => {
                    dispatch({ type: 'RECEIVE_DEPARTMENTS', companies: data });
                });

           dispatch({ type: 'REQUEST_DEPARTMENTS' });
        }
    },
    importData: (file: File): AppThunkAction<KnownAction> => (dispatch, getState) => {
        var data = new FormData()
        data.append('xmlFile', file);
        fetch('/XmlSerialize', {
            method: 'POST',
            body: data
        }).then(response => response.json() as Promise<Company[]>)
            .then(data => {
                dispatch({ type: 'RECEIVE_DEPARTMENTS', companies: data });
            });

        dispatch({ type: 'REQUEST_DEPARTMENTS' });
    }
};

// ----------------
// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

const unloadedState: DepartmentsState = { companies: [], isLoading: false };

export const reducer: Reducer<DepartmentsState> = (state: DepartmentsState | undefined, incomingAction: Action): DepartmentsState => {
    if (state === undefined) {
        return unloadedState;
    }

    const action = incomingAction as KnownAction;
    switch (action.type) {
        case 'REQUEST_DEPARTMENTS':
            return {
                companies: state.companies,
                isLoading: true
            };
        case 'RECEIVE_DEPARTMENTS':
            return {
                companies: action.companies,
                isLoading: false
            };           
    }

    return state;
};
