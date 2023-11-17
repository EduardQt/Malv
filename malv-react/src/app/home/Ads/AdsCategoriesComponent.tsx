import {Category_Mod} from "../../../network/Models/Category_Mod";
import {Row} from "reactstrap";

interface AdsCategoriesComponentProps {
    categories: Category_Mod[];
    setCategory: (categoryId: number) => void;
}

export const AdsCategoriesComponent = ({categories, setCategory}: AdsCategoriesComponentProps) => {

    const renderCategories = () => {
        return categories.map((value, index) => {
           return (
               <div className={'p-2 horizontal-divider'} role={"button"} onClick={() => setCategory(value.id)} key={index}>{value.title}</div>
           )
        });
    }

    return (
        <Row>
            <div style={{
                height: '70vh',
                overflowX: 'scroll'
            }}>
                {renderCategories()}
            </div>
        </Row>
    )
}
