import * as React from 'react';
import { connect } from 'react-redux';
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';
import { ApplicationState } from '../store';
import * as DepartmentsStore from '../store/Departments';

// At runtime, Redux will merge together...
type DepartmentProps =
    DepartmentsStore.DepartmentsState // ... state we've requested from the Redux store
    & typeof DepartmentsStore.actionCreators; // ... plus action creators we've requested
 // & RouteComponentProps<{ startDateIndex: string }>; // ... plus incoming routing parameters

interface RenderItemTitleProps
{ id: number, index: number, name: string, departmentType : DepartmentType}

enum DepartmentType {
    company = 0,
    department = 1,
    division = 2,
}

class RenderItemTitle extends React.PureComponent<RenderItemTitleProps, { isOver: boolean }> {
    constructor(props: RenderItemTitleProps) {
        super(props);
        this.state = { isOver: false };
        this.mouseOver = this.mouseOver.bind(this);
        this.mouseOut = this.mouseOut.bind(this);
        this.renderButtons = this.renderButtons.bind(this);
    }

    mouseOver() {
        this.setState({ isOver: true })
    }

    mouseOut() {
        this.setState({ isOver: false })
    }

    renderChangeModal() {
        alert("Тут должно быть модальное окно для изменения объекта с полями: Имя, Описание.")
    }

    renderDeleteModal() {
        alert("Тут должно быть модальное окно предупреждения о удалении.")
    }

    renderMoveModal() {
        alert("Тут должно быть модальное окно для перемещения объекта с полями в зависимости от типа сущности.")
    }

    renderButtons() {
        return (<div hidden={!this.state.isOver}>
            <button className="" onClick={this.renderChangeModal}>Изменить</button>
            <button className="" title="Удалить" onClick={this.renderDeleteModal}>Х</button>
            <button className="" title="Переместить" onClick={this.renderMoveModal}>{"<=>"}</button>
        </div>);
    }

    public render() {
        return (
            <div className="titleBox" onMouseOver={this.mouseOver} onMouseOut={this.mouseOut} >
                <div className="titleItem">{this.props.index + 1}. {this.props.name}</div> {this.renderButtons()}
            </div>)
    };
}

class FetchData extends React.PureComponent<DepartmentProps, { file: File | undefined }> {
    constructor(props: DepartmentProps) {
        super(props);
        this.state = { file: undefined };
        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

  // This method is called when the component is first added to the document
  public componentDidMount() {
    this.ensureDataFetched();
  }

  // This method is called when the route parameters change
  public componentDidUpdate() {
    //this.ensureDataFetched();
    }

    handleChange(event: any) {
        console.log(event.target.files[0]);
        this.setState({ file: event.target.files[0] });
    }

    handleSubmit(event: any) {
        if (this.state.file != undefined) {
            this.props.importData(this.state.file);
        }
    }

  public render() {
    return (
      <React.Fragment>
        <h1 id="tabelLabel">No-Name</h1>
            <p>Это приложение является тестовым заданием для компании No-Name.
                Функционал приложения не реализован до конца и является демонстрацией возможной архитектуры приложения по работе с иерархическим справочником подразделений.
            </p>
            <p>
                Реализованный функционал:
            </p>
            <ul>
                <li>
                    импорт и экспорт данных в xml файл.
                </li>
                <li>
                    отображение и обновление данных на странице
                </li>
                <li>
                    отображение кнопок при наведении на объект иерархического списка для изменения, удаления, перемещения.
                </li>
            </ul>
            <p>
                Для реализации приложения использовались:
            </p>
            <ul>
                <li>
                    .Net core 5.0
                </li>
                <li>
                    React.js
                </li>
                <li>
                    MS SQL database
                </li>
            </ul>
            {this.props.isLoading ? <div>Loading...</div> :
                <div>
                    <div className="dataForm">
                        <div className="dataFormItem">
                            <button onClick={() => {
                                alert("Тут должно быть модальное окно с выбором типа сущности и дополнительными полями:"
                                    + " Имя, Описание, и справочником родительских сущностей в зависимости от типа добавляемого объекта.")
                            }}>Добавить элемент</button>
                            <Link to="/XmlSerialize" target="_blank">
                                <button type="button">
                                    Экспорт данных в XML
                                </button>
                            </Link>
                        </div>
                        <div className="dataFormItem">
                            <form onSubmit={this.handleSubmit}>
                                <label>Загрузка данных из файла:</label>
                                <input type="file" typeof="xml" onChange={this.handleChange} />
                                <button type="submit" disabled={this.state.file == undefined}>Импорт данных</button>
                            </form>
                        </div>
                    </div>
                        {this.renderDepartments()}
                </div>}
      </React.Fragment>
    );
  }

  private ensureDataFetched() {
      if (this.props.companies == undefined || this.props.companies.length == 0) {
          this.props.requestDepartments();
      }
  }

  private renderDepartments() {
    return (<div className="parent">
                {this.props.companies.map((company: DepartmentsStore.Company, index: number) =>
                    <div key={company.companyId}>
                        <RenderItemTitle id={company.companyId} index={index} name={company.name} departmentType={DepartmentType.company} />
                        <div className="child">
                                {company.departments.map((department: DepartmentsStore.Department, index: number) => 
                                        <div key={department.departmentId}>
                                            <RenderItemTitle id={department.departmentId} index={index} name={department.name} departmentType={DepartmentType.department}/>
                                            <div className="child">
                                                        {department.divisions.map((division: DepartmentsStore.Division, index: number) =>
                                                            <div key={division.divisionId}>
                                                                <RenderItemTitle id={division.divisionId} index={index} name={division.name} departmentType={DepartmentType.division}/>
                                                            </div>
                                                        )}
                                            </div>
                                        </div>
                                    )}
                        </div>
                    </div>
                 )}
            </div>
    );
  }
}

export default connect(
  (state: ApplicationState) => state.departments, // Selects which state properties are merged into the component's props
  DepartmentsStore.actionCreators // Selects which action creators are merged into the component's props
)(FetchData as any); // eslint-disable-line @typescript-eslint/no-explicit-any
