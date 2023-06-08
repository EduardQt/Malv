import {Category_Mod} from "../../network/Models/Category_Mod";

export const searchCategories = (categories: Category_Mod[], query: string): Array<Category_Mod> => {
    return filter(categories, query.toLowerCase())
}

function filter(array: Category_Mod[], text: string): Category_Mod[] {
    const getNodes = (result: Category_Mod[], object: Category_Mod) => {
        if (object.title.toLowerCase().includes(text)) {
            result.push(object);
            return result;
        }
        if (Array.isArray(object.children)) {
            const children = object.children.reduce(getNodes, []);
            if (children.length) result.push({ ...object, children });
        }
        return result;
    };

    return array.reduce(getNodes, []);
}
