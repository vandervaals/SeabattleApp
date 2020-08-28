import { Cell } from './cell';

export class Ship {
    IsHorizontal: boolean;
    Cells: Cell[];

    constructor() {
        this.Cells = new Array<Cell>();
    }

    drawShip(pixelToOneStep: number, ctx: CanvasRenderingContext2D) {
        ctx.strokeStyle = 'blue';
        ctx.lineWidth = 5;

        const countOfDeck = this.Cells.length;
        const height = this.IsHorizontal === true ? pixelToOneStep : countOfDeck * pixelToOneStep;
        const width = this.IsHorizontal === true ? countOfDeck * pixelToOneStep : pixelToOneStep;

        ctx.strokeRect(this.Cells[0].x * pixelToOneStep, this.Cells[0].y * pixelToOneStep, width, height);
      }
}
