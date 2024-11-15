using System;


public interface IUserAction
{
	void GameOver();
	void GameStart();
	void GameRestart();
	void GameModeSet(bool mode);
}


