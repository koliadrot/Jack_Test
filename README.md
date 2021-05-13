# Jack
This is a test task. It contains a demo version for Windows.
### Link to Android - https://disk.yandex.ru/d/dKCWG_JtCK4sSQ
### Link to WebGl - https://play.unity.com/mg/other/jack-guvn

![Jack_Demo_Compressed](https://user-images.githubusercontent.com/31155244/118028168-0f9cca80-b363-11eb-94c1-ce17040dfb34.gif)

EN:
---
## This is a test project with top-down mechanics.

***The project used such design patterns as:***
* Factory
* Observer
* Singleton

Also, during the assignment, the basics of OOP were taken into account, such as:
* encapsulation
* polymorphism
* inheritance

The main character of the test project is a sphere that is moved by clicking the left mouse button.
The movement is implemented using MoveTowards, and its rotation to the target, through spherical interpolation (Slerp).
Obtaining coordinates to move to a given point, implemented through rays (RayCast).

There are interactive objects with which the character interacts through rays (RayCast) and a collision system (Collisions and Triggers).
***The project has 6 types of interactive objects:***
1. With description (Description)
2. Destroyable (Destroyable)
3. To gain experience (Experience)
4. Hit the character (Hit)
5. Interaction with which it is possible in the presence of a certain resource (Gate)
6. Teleport character to another scene (Gate)

5 and 6 types of interactive objects are combined in one object, which will teleport to another scene,
if the character has the required amount of experience.
Also, all interactive objects are scaled during the project, through the user interface system (UI_Canvas) and at
when they appear on the scene, ***the position coordinates are implemented in two ways:***
1. Random
2. ScriptableObject (InteractiveData.cs)

Interactive objects have an interactivity zone that displays the interaction of the character with this interactive object.
For clarity, the interactivity zone was made visible as a flat area and changed color upon interaction.
Different types of interactive objects can be combined in one object.

RU:
---
## Это тестовый проект с top-down механикой.

***В проекте были использованы такие паттерны проектировании как:***
* Фабрика
* Наблюдатель
* Одиночка

Так же, во время выполнения задания, были учтены основы ООП, такие как:
* инкапсуляция
* полиморфизм
* наследование

Главный персонаж тестового проекта, является сфера, которая перемещается с помощью клика левой кнопки мыши.
Движение реализовано с помощью MoveTowards , а его поворот к цели, через сферическую интерполяцию(Slerp).
Получение координат к перемещению к заданной точке, реализовано через лучи(RayCast).

Существуют интерактивные объекты, с которыми взаимодействует персонаж через лучи(RayCast) и систему столкновений(Collisions and Triggers).
***В проекте реализовано 6 типов интерактивных объектов:***
1. С описанием (Description)
2. Разрушаемые (Destroyable)
3. Для получения опыта (Experience)
4. Наносят удар персонажу (Hit)
5. Взаимодействие, с которым возможно при наличии определенного ресурса (Gate)
6. Телепорт персонажа в другую сцену (Gate)

5 и 6 типы интерактивных объектов объединены в одном объекте, которые телепортирую в другую сцену,
при наличии необходимого количества опыта у персонажа.
Так же все интерактивные объекты масштабируются по время проекта, через систему пользовательского интерфейса (UI_Canvas) и при 
появлении их на сцене, ***координаты положения реализуются двумя способами:***
1. Случайно
2. ScriptableObject(InteractiveData.cs)

У интерактивных объектов имеется зона интерактивности, которая отображает взаимодействие персонажа с данным интерактивным объектом.
Для наглядности, зона интерактивности была сделана видимой в виде плоской площади и изменяла цвет, при взаимодействии.
Разные типы интерактивных объектов могут комбинироваться в одном объекте.


