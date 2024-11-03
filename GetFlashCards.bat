@echo off

cd %~dp0

echo Current directory: %~dp0

if exist FlashCards (
	echo Update flashcards
	cd FlashCards
	git pull
) else (
	echo Download flashcards
	git clone https://github.com/FlashCard-Collection/Flashcards.git
)
