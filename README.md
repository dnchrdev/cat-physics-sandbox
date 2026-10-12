# Cat Physics Sandbox

Учебный проект на Clean Architecture. Игра — физическая песочница от первого лица: игрок — кот в доме, хватает и швыряет предметы, сбивает ими человека, который патрулирует комнаты, и выполняет квесты. Сборка под WebGL (Яндекс Игры), управление для ПК и мобильных.

![demo](Docs/demo.gif)

## Цель

Попрактиковаться в Clean Architecture: выстроить код вокруг игровых правил так, чтобы они не зависели от деталей реализации — движка, SDK, способа хранения данных — и их можно было менять и тестировать независимо от остального кода.

- **Логика на чистом C#.** Правила игры и сценарии живут отдельно от Unity, из движка в них используется только математика.
- **Движок — деталь реализации.** Логика обращается к внешнему миру через порты на языке игры, Unity подключается адаптерами, которые реализуют эти порты.
- **Зависимости направлены внутрь.** Фичи связаны через общие контракты в `Shared`.
- **Сборка в инсталлерах.** Все зависимости создаёт и связывает Zenject.

## Управление

| Действие | ПК |
|---|---|
| Движение | `W` `A` `S` `D` |
| Прыжок | `Space` |
| Обзор | мышь |
| Удар лапой / бросить предмет в руке | ЛКМ |
| Схватить / отпустить | ПКМ |
| Список квестов | `Q` |
| Подсказка к квесту (за рекламу) | `Z` |
| Настройки | `Tab` |

На мобильных — экранный джойстик, обзор свайпом и кнопки действий. Кнопки можно перемещать, скрывать и сбрасывать в редакторе раскладки.

## Стек

Unity 6 · C# · Zenject · UniTask · Addressables · Input System · AI Navigation · DOTween · PluginYG2

## Архитектура

Код разбит на фичи (`Player`, `Enemy`, `Interaction`, `Quests`, …). Каждая фича разделена на слои `Entities → UseCases → InterfaceAdapters → Infrastructure`, зависимости направлены внутрь.

- Правила игры (Entities, UseCases) не знают про Unity и Zenject. Всё внешнее — физика, NavMesh, анимация, случайность — доступно им только через порты, которые реализуют шлюзы.
- Фичи общаются через контракты в `Shared`.
- Зависимости собираются только в инсталлерах Zenject: `ProjectContext` → `SceneContext` → свой `GameObjectContext` на каждого врага.

## Структура

```
Assets/Features/
├── Player/ Camera/ Interaction/ Input/        персонаж, камера, захват и броски, ввод
├── Enemy/ BehaviourTree/                      ИИ врага, рэгдолл, спавн; своё дерево поведения
├── Quests/ Tutorial/                          квесты, подсказки, обучающий уровень
├── UI/ SettingsPanel/ MobileButtonsAdjustment/ MainMenu/
│                                              панели, настройки, редактор мобильной раскладки, меню
├── Storage/ Advertising/                      сохранения и реклама через SDK Яндекс Игр
├── Scene/ GameBootstrap/ GameLifeCycle/       загрузка сцен, запуск игры, старт сцены
├── Core/                                      логгер, время, пауза
└── Shared/                                    контракты между фичами
```

## Паттерны

| Паттерн | Как применён | Где смотреть |
|---|---|---|
| Dependency Injection, Composition Root | Зависимости собираются только в инсталлерах, внедрение через конструкторы. У каждого врага свой подконтейнер | [`EnemyInstaller`](Assets/Features/Enemy/Installer/EnemyInstaller.cs), [`EnemyFactory`](Assets/Features/Enemy/Infrastructure/Spawning/EnemyFactory.cs) |
| Ports & Adapters | Use case объявляет порт на своём языке, шлюз реализует его поверх Unity: `IEnemyMover` → `NavMeshAgent` | [`IEnemyMover`](Assets/Features/Enemy/UseCases/Ports/IEnemyMover.cs), [`EnemyMoverGateway`](Assets/Features/Enemy/InterfaceAdapters/Gateways/EnemyMoverGateway.cs) |
| Use Case (Interactor) | Один сценарий — один класс, без зависимостей от движка | [`PlayerRespawnUseCase`](Assets/Features/Player/UseCases/PlayerRespawnUseCase.cs) |
| MVP (Passive View) | Презентер реализует выходной порт use case'а и управляет вьюшкой через интерфейс. Вьюшка только показывает данные и сообщает о нажатиях | [`CurrentQuestPresenter`](Assets/Features/Quests/InterfaceAdapters/Presenters/CurrentQuestPresenter.cs), [`ICurrentQuestView`](Assets/Features/Quests/InterfaceAdapters/Ports/ICurrentQuestView.cs) |
| State | Режимы движения персонажа (ходьба, прыжок, полёт) — отдельные классы; `Enter` и `FixedTick` возвращают следующий режим | [`CharacterMovementUseCase`](Assets/Features/Player/UseCases/Movement/CharacterMovementUseCase.cs), [`Movement`](Assets/Features/Player/UseCases/Movement) |
| Behaviour Tree, Composite | `Selector` и `Sequence` — составные узлы над общим `CompositeNode`, листья — задачи врага | [`BehaviourTree`](Assets/Features/BehaviourTree/Entities), [`EnemyBehaviourTree`](Assets/Features/Enemy/InterfaceAdapters/BT/EnemyBehaviourTree.cs) |
| Strategy | Цели квестов взаимозаменяемы: квест считает прогресс, не зная, какая у него цель | [`Objectives`](Assets/Features/Quests/Entities/Objectives) |
| Template Method | `EachTargetOnceObjective` фиксирует правило «каждая цель засчитывается один раз», наследники определяют только `Matches` | [`EachTargetOnceObjective`](Assets/Features/Quests/Entities/Objectives/EachTargetOnceObjective.cs) |
| Null Object | Цель по умолчанию, которая ничего не засчитывает, вместо проверок на `null` | [`NoProgressObjective`](Assets/Features/Quests/Entities/Objectives/NoProgressObjective.cs) |
| Observer | События на портах; контроллеры подписываются в `Initialize` и отписываются в `Dispose` | [`IPlayerLifeEvents`](Assets/Features/Shared/Entities/Ports/IPlayerLifeEvents.cs) |
| Factory | Создание врагов из Addressables и сборка интерактивного предмета из сущности, шлюзов и презентера | [`EnemyFactory`](Assets/Features/Enemy/Infrastructure/Spawning/EnemyFactory.cs), [`InteractableFactory`](Assets/Features/Interaction/Infrastructure/Interactable/InteractableFactory.cs) |
| Repository | Квесты собираются из конфига и объектов сцены за портом `IQuestsRepository` | [`QuestsRepository`](Assets/Features/Quests/Infrastructure/Repositories/QuestsRepository.cs) |
| Object Pool | Карточки квестов не уничтожаются, а прячутся и переиспользуются | [`QuestCardsManager`](Assets/Features/Quests/Infrastructure/Managers/QuestCardsManager.cs) |
| DTO | Формат сохранения отделён от SDK и от игровых сущностей | [`SaveData`](Assets/Features/Storage/UseCases/Ports/SaveData.cs) |
