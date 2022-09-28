USE master;

USE Music;

DELETE FROM Artists;

INSERT INTO Artists (Name)

VALUES
('The Weeknd'),
('Mac Miller');

SELECT * FROM Artists;

SET QUOTED_IDENTIFIER OFF

INSERT INTO Songs (
    Title,
    ArtistID,
    Lyrics,
    Album
)
VALUES 
("Save Your Tears", 1, "Ooh\nNa-na, yeah\nI saw you dancing in a crowded room\nYou look so happy when I'm not with you\nBut then you saw me, caught you by surprise\nA single teardrop falling from your eye\nI don't know why I run away\nI'll make you cry when I run away\nYou could've asked me why I broke your heart\nYou could've told me that you fell apart\nBut you walked past me like I wasn't there\nAnd just pretended like you didn't care\nI don't know why I run away\nI'll make you cry when I run away\nTake me back 'cause I wanna stay\nSave your tears for another\nSave your tears for another day\nSave your tears for another day\nSo, I made you think that I would always stay\nI said some things that I should never say\nYeah, I broke your heart like someone did to mine\nAnd now you won't love me for a second time\nI don't know why I run away, oh, girl\nSaid I'll make you cry when I run away\nGirl, take me back 'cause I wanna stay\nSave your tears for another\nI realize that I'm much too late\nAnd you deserve someone better\nSave your tears for another day (ooh, yeah)\nSave your tears for another day (yeah)\nI don't know why I run away\nI'll make you cry when I run away\nSave your tears for another day, ooh, girl (ah)\nI said save your tears for another day (ah)\nSave your tears for another day (ah)\nSave your tears for another day (ah)", "After Hours"),
("Blue World", 2, "It's a blue world without you\nIt's a blue world alone\nYeah, well, this mad world made me crazy\nMight just turn around, do 180\nI ain't politickin', I ain't kissin' no babies\nThe devil on my doorstep bein' so shady\nMmm, don't trip\nWe don't gotta let him in, don't trip\nYeah, yeah\nI let it go, but I never go with it\nUh, yeah\nOkay, cool as fall weather\nFuck the bullshit, I'm here to make it all better\nWith a little music for you\nI don't do enough for you\nWithout you, it's the color blue\nOoh, don't trip\nI was in the city, they was talkin' that shit\nHad the homies with me, all a sudden, they split\nWe ain't even worried, we just laughin', that's rich\nYou know how it goes, it ain't broke, don't fix\nHey, one of these days we'll all get by\nDon't be afraid, don't fall\nThink I lost my mind, reality's so hard to find\nWhen the devil tryna call your line, but shit, I always shine\nEven when the light dim\nNo, I ain't God, but I'm feelin' just like Him\nOoh, don't trip\nSee, I was in the whip, ridin', me and my bitch\nWe was listenin' to us, no one else, that's it\nThat's a flex, just a bit, let me talk my shit\nSay my hand got bit\nYeah, well, this mad world made me crazy\nMight just turn around, do 180\nI ain't politickin', I ain't kissin' no babies\nThe devil on my doorstep bein' so shady\nMmm, don't trip\nWe don't gotta let him in, don't trip\nYeah, yeah\nI let it go, but I never go with it\nUh\nDon't trip\nDon't trip\nDon't trip\nWell, if you could see me now\nLove me and hold me down\nMy mind, it goes, it goes\nIt goes, it goes, it goes\nWell, this mad world made me crazy\nMight just turn around, do 180\nI ain't politickin', I ain't kissin' no babies\nThe devil on my doorstep bein' so shady\nMmm, don't trip\nWe don't gotta let him in, don't trip\nYeah, yeah\nI let it go, but I never go with it\nUh\nHey, one of these days we'll all get by\nDon't be afraid, don't fall in line", "Circles");

SELECT * FROM Songs;

SET QUOTED_IDENTIFIER ON

INSERT INTO Users (
    Username,
    EncryptedPassword,
    Role
)
VALUES
('useradmin','password', 'admin'),
('martinlovesmusic', '123456', ''),
('usernamecannotbeblank21','abc123', '');

SELECT * FROM Users;

SET QUOTED_IDENTIFIER OFF

INSERT INTO Comments (
    Content,
    Rating,
    UserId,
    SongID
)
VALUES
("I love this song! It's my go to workout song", 4, 3, 1),
('The production of this song is on another level!', 5, 2, 1);


SELECT * FROM Comments;