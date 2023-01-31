namespace ProjetoTransicao.Tests.Base;

public interface IFake<T>
{
    T GerarEntidadeValida();
    T GerarEntidadeInvalida();
}