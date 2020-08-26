import { Cell } from './cell';

export class Ship {
    isHorizontal: boolean;
    cells: Cell[];

    constructor() {
        this.cells = new Array<Cell>();
    }

    drawShip(pixelToOneStep: number, ctx: CanvasRenderingContext2D) {
        ctx.strokeStyle = 'blue';
        ctx.lineWidth = 5;

        const countOfDeck = this.cells.length;
        const height = this.isHorizontal === true ? pixelToOneStep : countOfDeck * pixelToOneStep;
        const width = this.isHorizontal === true ? countOfDeck * pixelToOneStep : pixelToOneStep;

        ctx.strokeRect(this.cells[0].x * pixelToOneStep, this.cells[0].y * pixelToOneStep, width, height);
      }
}
