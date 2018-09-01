import { MatPaginatorIntl } from "@angular/material";

export class MatPaginatorLocal extends MatPaginatorIntl {

    itemsPerPageLabel: string = "每页显示";
    /** A label for the button that increments the current page. */
    nextPageLabel: string = "下一页";
    /** A label for the button that decrements the current page. */
    previousPageLabel: string = "上一页";
    /** A label for the button that moves to the first page. */
    firstPageLabel: string = "首页";
    /** A label for the button that moves to the last page. */
    lastPageLabel: string = "尾页";
    /** A label for the range of items within the current page and the length of the whole list. */
    getRangeLabel: (page: number, pageSize: number, length: number) => string = (page, pageSize, length) => {
        if (length === 0 || pageSize === 0) {
            return '0 od ' + length;
        }
        length = Math.max(length, 0);
        const startIndex = page * pageSize;
        const endIndex = startIndex < length ?
            Math.min(startIndex + pageSize, length) :
            startIndex + pageSize;
        return '第' + (startIndex + 1) + ' - ' + endIndex + ' 条，总共：' + length + '条';
    };;

}