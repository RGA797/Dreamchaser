using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using UnityEngine;
using System.Collections.Generic;

//firebasemanager classe
//responsibility: managing database calls
public class FirebaseManager
{

    private FirebaseAuth auth;
    private FirebaseDatabase database;

    public FirebaseManager()
    {
        database = FirebaseDatabase.GetInstance(FirebaseApp.DefaultInstance.Options.DatabaseUrl.ToString());
        auth = FirebaseAuth.DefaultInstance;
    }

    //takes email and password parameters, and attempts sign in
    public void Login(string email, string password, Action<FirebaseUser> onSuccess, Action<string> onFailure)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                onFailure("Login was cancelled.");
                return;
            }
            if (task.IsFaulted)
            {
                onFailure("Login encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result;
            onSuccess(user);
        });
    }

    //registers a new user, with email and password. 
    public void Register(string email, string password, Action<FirebaseUser> onSuccess, Action<string> onFailure)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                onFailure("Registration was cancelled.");
                return;
            }
            if (task.IsFaulted)
            {
                onFailure("Registration encountered an error: " + task.Exception);
                return;
            }

            FirebaseUser user = task.Result;
            onSuccess(user);
        });
    }

    //retrieving questions from database. takes empty list of questions
    public void RetrieveQuestions(List<QuestionAndAnswer> questions, System.Action onCompletion)
    {
        //Debug.Log("Database URL: " + FirebaseApp.DefaultInstance.Options.DatabaseUrl);
        DatabaseReference databaseReference = database.RootReference;

        databaseReference.Child("Questions").GetValueAsync().ContinueWith(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to retrieve questions: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot childSnapshot in snapshot.Children)
                {
                    QuestionAndAnswer question = JsonUtility.FromJson<QuestionAndAnswer>(childSnapshot.GetRawJsonValue());
                    questions.Add(question);
                }
            }

            if (onCompletion != null)
            {
                onCompletion.Invoke();
            }
        });
    }

    //saves user data. should be using gamestatescript values in parameter
    public void SaveGame(string userId, float timer, float deathCount, int[] attempts, Action onSuccess, Action<string> onFailure)
    {
        DatabaseReference databaseReference = database.RootReference.Child("users").Child(userId);

        databaseReference.Child("timer").SetValueAsync(timer).ContinueWith(task => {
            if (task.IsFaulted)
            {
                onFailure("Failed to save timer to database: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                onSuccess();
            }
        });

        databaseReference.Child("deathCount").SetValueAsync(deathCount).ContinueWith(task => {
            if (task.IsFaulted)
            {
                onFailure("Failed to save death count to database: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                onSuccess();
            }
        });

        databaseReference.Child("attempts").SetValueAsync(attempts).ContinueWith(task => {
            if (task.IsFaulted)
            {
                onFailure("Failed to save attempts to database: " + task.Exception);
                return;
            }
            if (task.IsCompleted)
            {
                onSuccess();
            }
        });
    }

    //retrieves all user statistics from the database.
    public void RetrieveAllUserStatistics(Action<UserStatistic[]> onSuccess, Action<string> onFailure)
    {
        DatabaseReference databaseReference = database.RootReference.Child("users");
        Debug.Log("Retrieving all user statistics");
        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                onFailure("Failed to retrieve user statistics: " + task.Exception);
            }
            else if (task.IsCompleted)
            {
                Debug.Log("Got user statistics");
                DataSnapshot snapshot = task.Result;
                List<UserStatistic> userStatistics = new List<UserStatistic>();

                foreach (DataSnapshot userSnapshot in snapshot.Children)
                {
                    if (userSnapshot.HasChild("timer") && userSnapshot.HasChild("deathCount") && userSnapshot.HasChild("attempts"))
                    {
                        string userId = userSnapshot.Key;
                        Debug.Log("userId: " + userId);

                        float timer = 0;
                        if (userSnapshot.Child("timer").Value != null)
                        {
                            timer = float.Parse(userSnapshot.Child("timer").Value.ToString());
                            Debug.Log("timer: " + timer);
                        }

                        float deathCount = 0;
                        if (userSnapshot.Child("deathCount").Value != null)
                        {
                            deathCount = float.Parse(userSnapshot.Child("deathCount").Value.ToString());
                            Debug.Log("deathCount: " + deathCount);
                        }

                        List<int> attempts = new List<int>();
                        DataSnapshot attemptsSnapshot = userSnapshot.Child("attempts");

                        foreach (DataSnapshot attemptSnapshot in attemptsSnapshot.Children)
                        {
                            int attemptValue = int.Parse(attemptSnapshot.Value.ToString());
                            attempts.Add(attemptValue);
                            Debug.Log("Attempt: " + attemptValue);
                        }

                        Debug.Log("attempts count: " + attempts.Count);

                        UserStatistic userStatistic = new UserStatistic(userId, timer, deathCount, attempts.ToArray());
                        userStatistics.Add(userStatistic);
                    }
                }

                onSuccess(userStatistics.ToArray());
            }
        });
    }

}

