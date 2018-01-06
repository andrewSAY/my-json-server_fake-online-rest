namespace TestTaskWevService.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий основные методы DI-контейнера в проекте
    /// </summary>
    public interface IDependencyResolver
    {
        /// <summary>
        /// Регистрирует объект какого конкертного типа должен возвращаться при запросе объекта с типом, реализующим более высокий уровень абстракции
        /// </summary>
        /// <typeparam name="TDefinition">Тип объекта с более высоким уровнем абстракции</typeparam>
        /// <typeparam name="TImplemenation">Тип объекта, реализующий абстракцию</typeparam>
        void Register<TDefinition, TImplemenation>() where TImplemenation: TDefinition;
        /// <summary>
        /// Регистрирует объект какого конкертного типа должен возвращаться при запросе объекта с типом, реализующим более высокий уровень абстракции.
        /// Конкретный тип будет представлен всегда одним и тем же объектом при каждом запросе.
        /// </summary>
        /// <typeparam name="TDefinition">Тип объекта с более высоким уровнем абстракции</typeparam>
        /// <typeparam name="TImplemenation">Тип объекта, реализующий абстракцию</typeparam>
        void RegisterAsSingltone<TDefinition, TImplemenation>() where TImplemenation : TDefinition;
        /// <summary>
        /// Возвразщает объект, представленный типом, реализующим запрошенный уровень абстракции
        /// </summary>
        /// <typeparam name="TDefinition">Тип объекта с более высоким уровнем абстракции</typeparam>
        /// <returns></returns>
        TDefinition Resolve<TDefinition>();
    }
}
