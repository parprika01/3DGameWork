using System;


public interface IUserAction
{
	bool GameOver();
	bool Win();
	void GameStart();
	void GameRestart();
	void Pause();
	void Resume();
	int GetRemainTime();
}


