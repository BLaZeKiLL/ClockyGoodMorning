namespace CodeBlaze.Systems {

    public static class TickUtils {

        public static int SecToTicks(int sec) => (int) (sec * (1f / TickSystem.TICK_TIMER));

        public static int TicksToSec(int ticks) => (int) (ticks / (1f / TickSystem.TICK_TIMER));

    }

}